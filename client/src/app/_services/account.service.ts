import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

// Injectable allows this service to be injected into other components or services in the app.
@Injectable({
  // Metadata is the providedIn aspect
  providedIn: 'root'
})
export class AccountService {
  // Set to something is =, While the : sets something to a type of something
      // Baseurl is the application base can be redirected to users
  baseUrl = 'https://localhost:5001/api/';

  // replaySubject is a special type of observable. Stores however many values you assing it by (), storing 1 as of now.
      // Anytime subscriber subscribes to this observable, emit last value inside it or however many values we assign it to emit.
          // $ in name is convention for observable.
            // Observable can be observed by other components/clases in the application. 
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  //Inject HTTP client into Account Service
  constructor(private http: HttpClient) { }

  // Login to receive credentials from the login form from navbar
  login(model: any)
  {
    // Post expects the url to post to which will end up being https://localhost:5001/api/account/login
      // Will return model which has both username and password to that link.
        // Pipe is rxjs and can run multiple functions into a single one which is the pipe.
          // Must add the <User> to the HTTP post request since it is returning that type or else massive error
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        //Const is block scoped verus a var which is globally scoped
        const user = response;
        if(user) {
          // console.log(JSON.stringify({ x: 5, y: 6 }));
            // expected output: "{"x":5,"y":6}". This turns var x = 6 into JSON or "x" = 6;
              // sets string user to string or JSON of const user
          localStorage.setItem('user', JSON.stringify(user))
          // Sets next value inside here. .next() fires off event that all subscribers will listen to
          this.currentUserSource.next(user);
        }
      }) 
    )
  }

  // Not retruning inside being the map() method vbut rather the outside or the post method.
    // Would have to return user inside map() method in order to get the inside values and not just the post. 
  register(model: any) {
    //Must specify the type of return value as User interface and set var object equal to interface User
    return this.http.post<User>(this.baseUrl + "account/register", model).pipe(
      map((user: User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
          // Sends error as of right now with the user variable not being set to User which is the typescript interface
          // Since currentUserSource is being used, expects User by creating of it up top
        }
        return user;
      })
    )
  }

  setCurrentUser(user: User)
  {
    this.currentUserSource.next(user);
  }

  logout()
  {
    // Removes "user" JSON with its stringify value
    localStorage.removeItem('user')
    // He uses null but undefined can replace null because null cannot be a parameter. 
    this.currentUserSource.next(undefined);
  }
}
