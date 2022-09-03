import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { MyTimeOffComponent } from './components/my-time-off/my-time-off.component';
import { AboutTimeOffComponent } from './components/about-time-off/about-time-off.component';
import { TeamTimeOffComponent } from './components/team-time-off/team-time-off.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MyTimeOffComponent,
    AboutTimeOffComponent,
    TeamTimeOffComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
