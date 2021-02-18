﻿import { Injectable, Inject, EventEmitter } from '@angular/core';
import { isNullOrEmptyString, hasValue } from './helper.service';
import { Router } from '@angular/router';
import { User } from '../../core/models/user.model';

@Injectable()
export class SessionService {

    private token: string;
    private tokenKey: string = "c__tk";
    private roleKey: string = "c__rl";
    public currentUser = new User();

    constructor(private router: Router) {
    }

    public setToken(token: string) {
        this.setValue(this.tokenKey, token);
    }
    public setUserRole(role: number) {
        this.setValue(this.roleKey, role);
    }
    public getUserRole(): number {
        return Number(this.getValue(this.roleKey));
    }
    public getToken(): string {
        return this.getValue(this.tokenKey);
    }

    private setValue(key: string, value: any) {
        localStorage.setItem(key, value);
    }

    private getValue(key: string) {
        if (isNullOrEmptyString(localStorage.getItem(key))) {
            return null;
        }

        switch (key) {
            case this.tokenKey:
                if (isNullOrEmptyString(this.token)) {
                    this.token = localStorage.getItem(key);
                }
                return 'Bearer ' + this.token;

            case this.roleKey:
                return localStorage.getItem(key);
        }
    }

    public clear() {
        localStorage.removeItem(this.tokenKey);
        this.token = "";
    }

    public signOutWithErrorMessage(errMessage) {
        this.clear();
        if (hasValue(errMessage)) {
            this.router.navigate(['/login', { errormessage: errMessage }]);
        } else {
            this.router.navigateByUrl("/login");
        }
    }

    
}