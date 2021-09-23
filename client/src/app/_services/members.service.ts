import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

        // No Longer needed because the jwt.interceptor.ts works the same but doesnt give error.
// const httpOptions = {
//   headers: new HttpHeaders({
//     // Temporary solution to Authorization of members 
//     Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  //Same as account.service.ts - check environment files for code url
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers() {
      // Getting members requires that not specified as just return of Member but as Member[] array
    return this.http.get<Member[]>(this.baseUrl + 'users')
  }

  getMember(username: string | null){
      // Gets single Member and uses url from postman that returns a single user with httpOptions temporary solution
        //Param of username set to string, very simple and easy to understand
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }
}
