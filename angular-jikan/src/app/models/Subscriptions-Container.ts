import { Subscription } from "rxjs";

export class SubscriptionsContainer {
    private subs : Subscription[] = [];

    /**
    * Adds a Subcription to the subs array.
    *
    * @param s - The Subscription to add.
    *
    */
    set add(s: Subscription) {
        this.subs.push(s);
    }

    /**
    * Unsubscribes from all of the Subscriptions in the subs array.
    */
    dispose(): void {
        this.subs.forEach(s => s.unsubscribe());
    }
}