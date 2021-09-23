import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path:'',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children :[
      // All Children are covered by the canActivate: [AuthGuard], protects all paths with angular route guard
      {path: 'members', component: MemberListComponent, },
      // Changed from members/:id to members/:username so params work inside the member detail component
      {path: 'members/:username', component: MemberDetailComponent},
      {path: 'lists', component: ListsComponent},
      {path: 'messages', component: MessagesComponent}   
    ]
  },
  // Not found component link with a horrible looking page design, see src/app/errors/not-found
  {path:'not-found', component: NotFoundComponent},
  // Server Error component link with good page design and lots of issues i=when redidrected if get bad server-error. See scr/app/errors/server-error
  {path:'server-error', component: ServerErrorComponent},
  // This is for the errors via the buggy controller and test-errors.component.ts and test-errors.component.html
  {path:'errors', component : TestErrorsComponent},
    // If the user goes to localhost:4200/member and not localhost:4200/members, then it will redirect back to Home Component
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
