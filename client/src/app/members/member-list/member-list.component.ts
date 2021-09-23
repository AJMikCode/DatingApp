import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  // Error is because it only sets the type to members and not equal to any value.
  members: Member[] | undefined;

  //add members service to imported inside constructor.
  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    //Subscribes to members 
    this.memberService.getMembers().subscribe( members => {
      this.members = members;
    })
  }

}
