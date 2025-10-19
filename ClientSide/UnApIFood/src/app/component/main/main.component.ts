import { RouterOutlet } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';



@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})


export class MainComponent implements OnInit {
  user: any; // Variable para almacenar los datos del usuario
  favoritePlaces: any[] = [];
  selectedPlace: any;
  places: any[] = [];
  showFavoriteTable: boolean = false;
  showTable: boolean = false;
  showUsersTable: boolean = false;
  users: any[] = [];

  toggleTableFav() {
    this.showFavoriteTable = true;
    this.showTable = false;
    this.showUsersTable = false
  }

  toggleTable() {
    this.showTable = true;
    this.showFavoriteTable = false;
    this.showUsersTable = false
  }

  toggleUserTable() {
    this.showTable = false;
    this.showFavoriteTable = false;
    this.showUsersTable = true
  }


  constructor(private http: HttpClient) {
    this.user = null;
    
  }

  ngOnInit() {
    const userEmail = localStorage.getItem('email') || '';

    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });

    const requestOptions = {
      headers: headers
    };

    this.http.get<any[]>('http://localhost:5016/API/v1/Users', requestOptions).subscribe(
      (res: any[]) => {
        const sortedUsers = res.sort((a, b) => a.email.localeCompare(b.email));
        this.user = sortedUsers.find(user => user.email === userEmail);

      },
      (error: HttpErrorResponse) => {
        console.error(error);
      }
    );
  }

  getFavoritePlaces(userId: number) {
    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });
  
    const requestOptions = {
      headers: headers
    };
  
    const url = `http://localhost:5016/API/v1/UsersFav/user/${userId}`;
    this.http.get(url, requestOptions).subscribe(
      (response: any) => {
        if (response && Array.isArray(response)) {
          const favoritePlaces = response;
  
          const placeObservables = favoritePlaces.map((favorite: any) => {
            const placeUrl = `http://localhost:5016/API/v1/Places/${favorite.placeId}`;
            return this.http.get(placeUrl, requestOptions);
          });
  
          forkJoin(placeObservables).subscribe(
            (placeResponses: any[]) => {
              this.favoritePlaces = favoritePlaces.map((favorite, index) => {
                return {
                  id: favorite.id,
                  userId: favorite.userId,
                  placeId: favorite.placeId,
                  placeInfo: placeResponses[index] // Ajustar esto según la estructura real de la respuesta del lugar
                };
              });
              this.selectedPlace = this.favoritePlaces[0] ? this.favoritePlaces[0].placeInfo : null;
            },
            (error) => {
              console.error(error);
            }
          );
        } else {
          console.error('La respuesta del servidor no está en el formato esperado');
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getPlaces() {
    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });
  
    const requestOptions = {
      headers: headers
    };
    const url = 'http://localhost:5016/API/v1/Places';

    this.http.get(url, requestOptions).subscribe(
      (response: any) => {
        this.places = response; // Asignamos los lugares de comida a la variable places
      },
      (error) => {
        console.error(error);
      }
    );
  }

  DeleteFavorite(placeId: number) {
    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });

    const requestOptions = {
      headers: headers
    };

    const url = `http://localhost:5016/API/v1/UsersFav?id=${placeId}`;

    this.http.delete(url, requestOptions).subscribe(
      () => {
        // Actualizar la lista de lugares favoritos después de eliminar uno
        this.getFavoritePlaces(this.user.id);
      },
      (error: HttpErrorResponse) => {
        console.error(error);
      }
    );
  }


  AddFavorite(placeId: number, userId: number) {
    const headers = new HttpHeaders({
      'ApiKey': '123456',
      'Content-Type': 'application/json'
    });

    const requestOptions = {
      headers: headers
    };

    const body = {
      placeId: placeId,
      userId: userId
    };

    this.http.post('http://localhost:5016/API/v1/UsersFav', body, requestOptions).subscribe(
      () => {
        // Actualizar la lista de lugares favoritos después de añadir uno
        this.getFavoritePlaces(this.user.id);
      },
      (error: HttpErrorResponse) => {
        console.error(error);
      }
    );
  }
  

  // Bono?
  getAllUsers() {
    const headers = new HttpHeaders({
      'ApiKey': '123456'
    });
  
    const requestOptions = {
      headers: headers
    };
    this.http.get('http://localhost:5016/API/v1/Users', requestOptions).subscribe(
      (response: any) => {
        // Aquí puedes procesar los datos de respuesta y almacenarlos en una variable para mostrarlos en la tabla.
        this.users = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
