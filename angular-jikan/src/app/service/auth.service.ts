import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from '../models/LoginRequest';
import { RegisterUserRequest } from '../models/RegisteredUserRequest';
import { UserCredentials } from '../models/UserCredentials';
import { Output, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  userCreds : UserCredentials | null = null;
  @Output() loggedInEvent : EventEmitter<boolean> = new EventEmitter<boolean>();

  baseUrl : string = "https://localhost:44302/api/user"
  httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json"})}

  constructor(private http : HttpClient) { }

  /**
  * Sends a Post request to register a user.
  *
  * @param user - The user to be registered.
  * @returns  true if successfull registered, false otherwise.
  *
  */
  registerUser(user : RegisterUserRequest) : Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl, user, this.httpOptions);
  }

  /**
  * Sends a Post request to login in a user.
  *
  * @param user - The login credentials for the user attempting to login. 
  *
  */
  loginUser(user : LoginRequest): void {
    this.http.post<UserCredentials>(this.baseUrl + "/login", user, this.httpOptions)
    .subscribe(c => this.saveUserAndToken(c));
  }

  /**
  * Sets the loggedInEvent and saves the user credentials (username, token) to userCreds for the logged in user.
  *
  * @param toSave - the UserCredentials to be saved.
  *
  */
  saveUserAndToken(toSave : UserCredentials): void {
    this.loggedInEvent.emit(true);
    this.userCreds = toSave;
  }

  /**
  * Checks if a user is currently signed in.
  *
  * @returns true if signed in, false otherwise.
  * 
  */
  isSignedIn(): boolean {
    return this.userCreds != null;
  }

  /**
  * Signs out the user who is currently signed in and updates the loggedInEvent.
  */
  signOut(): void {
    this.userCreds = null;
    this.loggedInEvent.emit(false);
  }

}
