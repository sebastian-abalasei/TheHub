import {Inject, Injectable} from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeInterceptor implements HttpInterceptor {
  loginUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.loginUrl = `${baseUrl}/authentication/login`;
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && (error.status == 401 || error.url?.startsWith(this.loginUrl))) {
          window.location.href = `${this.loginUrl}?returnUrl=${window.location.pathname}`;
        }
        return throwError(() => error);
      }),
      // HACK: As of .NET 8 preview 5, some non-error responses still need to be redirected to login page.
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse && event.url?.startsWith(this.loginUrl)) {
          window.location.href = `${this.loginUrl}?returnUrl=${window.location.pathname}`;
        }
        return event;
      }));
  }
}
