import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { User } from '../../../entities/user';
import { UserService } from '../../../services/common/models/user.service';
import { Create_User } from '../../../contracts/users/create_user';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private userService: UserService,
    private toastService: CustomToastrService
  ) { }

  frm: FormGroup;

  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      nameSurname: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)
      ]],
      userName: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)
      ]],
      email: ["", [
        Validators.required,
        Validators.maxLength(250),
        Validators.email
      ]],
      password: ["", [
        Validators.required,

      ]],
      confirmPassword: ["", [
        Validators.required,

      ]]
    }, {
      validators: (group: AbstractControl): ValidationErrors | null => {

        let password = group.get("password").value;
        let confirmPassword = group.get("confirmPassword").value
        return password === confirmPassword ? null : { notSame: true };
      }
    })
  }

  // get -- property oldugun bildirir
  get component() {
    return this.frm.controls;
  }

  submitted: boolean = false;

  async onSubmit(user: User) {
    this.submitted = true;

    if (this.frm.invalid)
      return;

    const result: Create_User = await this.userService.create(user);

    if(result.succeeded)
      this.toastService.message(result.message, "User created success!!!", {
    messageType: ToastrMessageType.Success,
    position: ToastrPosition.TopRight
    })
    else
    this.toastService.message(result.message, "User created not success!!!", {
      messageType: ToastrMessageType.Error,
      position: ToastrPosition.TopRight
      })
  }
}
