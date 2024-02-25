import {APP_ID, APP_INITIALIZER, NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import {ModalModule} from 'ngx-bootstrap/modal';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {TodoComponent} from './todo/todo.component';
import {AuthorizeInterceptor} from './api-authorization/authorize.interceptor';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {UnauthorizedUserComponent} from "./error-pages/unauthorized-user/unauthorized-user.component";
import {NotFoundComponent} from "./error-pages/not-found/not-found.component";
import {AuthGuard} from "./api-authorization/auth-guard";
import {AuthState} from "./api-authorization/auth-state";


// add a provider to array of providers
const CoreProviders = [
  {
    provide: APP_INITIALIZER,
    // dummy factory
    useFactory: () => () => {
    },
    multi: true,
    // injected depdencies, this will be constructed immidiately
    deps: [AuthState],
  },
  // you might want to add a configuration service
  // add interceptors
  {
    provide: HTTP_INTERCEPTORS,
    multi: true,
    useClass: AuthorizeInterceptor,
  },
  {provide: APP_ID, useValue: 'the-crowd'}
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'counter', component: CounterComponent},
      {path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard]},
      {path: 'todo', component: TodoComponent, canActivate:[AuthGuard], canActivateChild:[AuthGuard]},
      {
        path: 'authentication',
        loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
      },
      {
        path: 'user',
        loadChildren: () => import('./user/user.module').then(m => m.UserModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
        canActivate: [AuthGuard]
      },
      {
        path: "unauthorized-user",
        component: UnauthorizedUserComponent
      },
      {path: '404', component: NotFoundComponent},
      {path: '**', redirectTo: '/404', pathMatch: 'full'}
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [...CoreProviders],
  bootstrap: [AppComponent]
})
export class AppModule {
}
