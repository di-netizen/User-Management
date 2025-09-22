import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent {
  loginForm: FormGroup;
  submitted = false;
  error = "";

  constructor(private fb: FormBuilder, private router: Router) {
    this.loginForm = this.fb.group({
      username: ["", [Validators.required]],
      password: ["", [Validators.required]]
    });
  }

  // helper for template access (avoids TS index-signature error)
  get f(): any {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.error = "";
    if (this.loginForm.invalid) return;
    const { username, password } = this.loginForm.value;
    if (username === "admin" && password === "admin") {
      // success — go to users page
      this.router.navigate(["/users"]);
    } else {
      this.error = "Invalid username or password (try admin/admin).";
    }
  }
}
