
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

export class Login{
  email: string;
  password: string;
  
  constructor(){
    this.email = '';
    this.password = '';
  }
}

export class User{
  id: number;
  role: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  lastLogin: Date;
  registeredOn: Date;

  
  constructor(){
    this.id = 0;
    this.role = '';
    this.email = '';
    this.password = '';
    this.firstName = '';
    this.lastName = '';
    this.lastLogin = new Date();
    this.registeredOn = new Date();
  }
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {

  loginObj: Login;
  userObj: User;
  errorMessage: string;

  constructor(private http: HttpClient, private router: Router){
    this.loginObj = new Login();
    this.userObj = new User();
    this.errorMessage = '';
  }

  
  //////////////////////////////////////////////////////////////////////////////
  // Hacer inicio de sesión
  onLogin() {
    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });

    const requestOptions: any = {
      headers,
      responseType: 'text' // Cambia el tipo de respuesta a texto
    };

    this.http.post('http://localhost:5016/API/v1/Login', this.loginObj, requestOptions).subscribe(
      (res: any) => {
        const token = res;
        if (token) {
          localStorage.setItem('token', token);
          localStorage.setItem('email', this.loginObj.email);

          // Ir a la página principal
          this.router.navigateByUrl('/main');
        } else {
          alert('Credenciales inválidas');
        }
      },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          alert('Credenciales inválidas');
        } else {
          console.error(error);
        }
      }
    );
  }

  //////////////////////////////////////////////////////////////////////////////
  // Realizar Registro
  onSignup() {
    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });

    this.http.post('http://localhost:5016/API/v1/Users', this.userObj, { headers }).subscribe(
      (res: any) => {
        if (res) {
        // Manejar la respuesta del servidor después de crear el usuario
        // Puedes redirigir a una página de inicio de sesión o realizar otras acciones necesarias
        alert('Usuario creado exitosamente');
        this.router.navigateByUrl('/login');
        } else {
          alert('Ha ocurrido un error :/');
        }
      },
      (error: HttpErrorResponse) => {
        console.error(error);
        // Manejar los errores de la solicitud
        this.errorMessage = 'Error al crear el usuario. Por favor, inténtelo de nuevo.';
      }
    );
  }
}



