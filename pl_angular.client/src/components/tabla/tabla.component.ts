import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment.development';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-tabla',
  templateUrl: './tabla.component.html',
  styleUrls: ['./tabla.component.css']
})
export class TablaComponent implements OnInit {

  Usuarios: any = [];
  API_URI = environment.apiUrl;
  toastr: any;

  usuario = {
    Nombre: '',
    ApellidoPaterno: '',
    ApellidoMaterno: '',
    Edad: ''
  };

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.GetAll();
  }

  GetAll() {
    this.http.get(this.API_URI + '/Usuario').subscribe(
      (result) => {
        console.log(result);
        this.Usuarios = result;
      },
      (error) => console.error(error)
    );
  }

  Delete(IdUsuario: Int32Array) {
    Swal.fire({
      title: '¿Estas seguro de eliminar el usuario?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.http.delete(this.API_URI + '/Usuario/' + IdUsuario).subscribe(
          (res) => {
            //Llena el arreglo con la respuesta que enviamos
            console.log(res);
            console.log('Usuario eliminado con éxito');
            this.GetAll();
            this.toastr.warning(
              'Usuario eliminado con éxito',
              'Usuario eliminado'
            );
          },
          (err) => console.error(err)
        );
      }
    });
  }
}
