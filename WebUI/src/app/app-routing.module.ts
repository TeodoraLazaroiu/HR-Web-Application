import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { MyTimeOffComponent } from './components/my-time-off/my-time-off.component';
import { AboutTimeOffComponent } from './components/about-time-off/about-time-off.component';
import { TeamTimeOffComponent } from './components/team-time-off/team-time-off.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {path: "", component:HomeComponent},
  {path: "home", component:HomeComponent},
  {path: "my-time-off", component:MyTimeOffComponent},
  {path: "about-time-off", component:AboutTimeOffComponent},
  {path: "team-time-off", component:TeamTimeOffComponent},
  {path: "login", component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
