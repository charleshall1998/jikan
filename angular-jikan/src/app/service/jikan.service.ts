import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Watch } from '../models/Watch';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class JikanService {

  baseUrl : string = "https://localhost:44302/api"
  httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json"})}

  constructor(private http : HttpClient) { }

  /**
  * Sends a Get request to get all Watches.
  * 
  * @returns An Observable of Watch array containing all Watches.
  * 
  */
  getAllWatches(): Observable<Watch[]> {
    return this.http.get<Watch[]>(this.baseUrl + "/watch");
  }

  /**
  * Sends a Get request to get a Watch by id.
  * 
  * @param id - The id to get by.
  * @returns An Observable of Watch containing the Watch with the associated id.
  * 
  */
  getWatchById(id : number): Observable<Watch> {
    return this.http.get<Watch>(this.baseUrl + "/watch/" + id);
  }

  /**
  * Sends a Get request to get a Watch by name.
  * 
  * @param name - The name to get by.
  * @returns An Observable of Watch containing the Watch with the associated name.
  * 
  */
  getWatchByName(name : string): Observable<Watch> {
    return this.http.get<Watch>(this.baseUrl + "/watch/name/" + name);
  }

  /**
  * Sends a Get request to get Watches by type.
  * 
  * @param type - The type to get by.
  * @returns An Observable of Watch array containing the Watches with the associated type.
  * 
  */
  getWatchesByType(type : string): Observable<Watch[]> {
    return this.http.get<Watch[]>(this.baseUrl + "/watch/type/" + type);
  }

  /**
  * Sends a Get request to get Watches by order id.
  * 
  * @param orderId - The order id to get by.
  * @returns An Observable of Watch array containing the Watches with the associated order id.
  * 
  */
  getWatchesByOrderId(orderId : number): Observable<Watch[]> {
    return this.http.get<Watch[]>(this.baseUrl + "/watch/order/" + orderId);
  }

  /**
  * Sends a Get request to get quantities by order id.
  * 
  * @param orderId - The order id to get by.
  * @returns An Observable of number array containing the quantities with the associated order id.
  * 
  */
  getWatchQuantityByOrderId(orderId : number): Observable<number[]> {
    return this.http.get<number[]>(this.baseUrl + "/watch/order/quantity/" + orderId);
  }

  /**
  * Sends a Post request to add an Order.
  * 
  * @param toAdd - The Order to be added.
  * @returns An Observable of Order containing the Order to be added..
  * 
  */
  addOrder(toAdd : Order): Observable<Order> {
    return this.http.post<Order>(this.baseUrl + "/order", toAdd, this.httpOptions);
  }

  /**
  * Sends a Get request to get all Orders.
  * 
  * @returns An Observable of Order array containing all Orders.
  * 
  */
  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + "/order");
  }

  /**
  * Sends a Get request to get Order by id.
  * 
  * @param id - The id to get by.
  * @returns An Observable of Order containing the Order with the associated id.
  * 
  */
  getOrderById(id : number): Observable<Order> {
    return this.http.get<Order>(this.baseUrl + "/order/" + id);
  }

}
