import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/Operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
    intercept(req:HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        return next.handle(req).pipe(
            catchError(error => {
                let modelStateError = '';
                if (error instanceof HttpErrorResponse) {
                    if (error.status === 401) {
                        return throwError(error.statusText);
                    }
                    const applicationError = error.error;
                    if (applicationError) {
                        if (typeof applicationError === 'object') {
                            for (const key in applicationError) {
                                if (applicationError[key]) {
                                    modelStateError += applicationError[key] + '\n'
                                }
                            }
                        }
                        else {
                            modelStateError = applicationError;
                        }
                       
                    }
                }



                return throwError(modelStateError || "SERVER ERROR");
            })
        );
        
    }
}


export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi:true
}