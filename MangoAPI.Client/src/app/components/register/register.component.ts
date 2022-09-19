import {Component, OnDestroy} from '@angular/core';
import {Router} from "@angular/router";
import {RegisterCommand} from 'src/app/types/requests/RegisterCommand';
import {UsersService} from "../../services/api/users.service";
import {ValidationService} from "../../services/messenger/validation.service";
import {ErrorNotificationService} from "../../services/messenger/error-notification.service";
import {RoutingConstants} from "../../types/constants/RoutingConstants";
import {Subject, takeUntil} from "rxjs";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnDestroy {
  constructor(private _router: Router,
              private _usersService: UsersService,
              private _validationService: ValidationService,
              private _errorNotificationService: ErrorNotificationService) {
  }

  public registerCommand: RegisterCommand = {
    displayName: "",
    email: "",
    password: "",
    termsAccepted: false,
  };

  componentDestroyed$: Subject<boolean> = new Subject();

  public get routingConstants(): typeof RoutingConstants {
    return RoutingConstants;
  }

  ngOnDestroy(): void {
    this.componentDestroyed$.next(true);
    this.componentDestroyed$.complete();
  }

  onRegisterClick(): void {
    let displayNameFieldValidationResult = this._validationService.validateField(this.registerCommand.displayName, 'Display name');
    let emailFieldValidationResult = this._validationService.validateField(this.registerCommand.email, 'Email');
    let passwordFieldValidationResult = this._validationService.validateField(this.registerCommand.password, 'Password');

    if(!displayNameFieldValidationResult || !emailFieldValidationResult || !passwordFieldValidationResult) {
      return;
    }

    if (!this.registerCommand.termsAccepted) {
      alert('Terms not accepted');
      return;
    }

    this._usersService.createUser(this.registerCommand).pipe(takeUntil(this.componentDestroyed$)).subscribe({
      next: _ => {
        this._router.navigateByUrl(this.routingConstants.CheckEmailNote).then(r => r);
      },
      error: error => {
        this._errorNotificationService.notifyOnError(error);
      }
    });
  }
}
