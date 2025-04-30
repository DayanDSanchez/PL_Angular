import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router'; // Importa Router
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment.development';

import Swal from 'sweetalert2';

// Define la interfaz para el usuario
interface Usuario {
  idUsuario?: number;
  Nombre: string;
  ApellidoPaterno: string;
  ApellidoMaterno: string;
  Imagen?: string;
  FechaNacimiento: string;
}

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})

export class FormComponent {

  API_URI = environment.apiUrl;

  // Objeto usuario con la interfaz
  usuario: Usuario = {
    Nombre: '',
    ApellidoPaterno: '',
    ApellidoMaterno: '',
    Imagen: '',
    FechaNacimiento: ''
  };

  archivoSeleccionado: File | null = null; // <-- Agregas esta propiedad para guardar el archivo
  imagenPreview: string | ArrayBuffer | null = null;

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute // Para obtener parámetros de la URL
  ) { }

  ngOnInit() {
    const idUsuario = this.route.snapshot.params['idUsuario'];
    if (idUsuario) {
      this.GetUsuarioById(Number(idUsuario)); // Convertimos a int
    }
  }

  // Aquí agregas el método del archivo
  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.archivoSeleccionado = file;

      const reader = new FileReader();
      reader.onload = (e: any) => {
        const base64 = e.target.result.split(',')[1];
        this.usuario.Imagen = base64;
        this.imagenPreview = e.target.result; // <-- Aquí actualizamos la imagen en el formulario
      };
      reader.readAsDataURL(file);
    }
  }


  AddOrUpdateUsuario() {
    if (this.usuario.idUsuario) {
      //Actualiza
      this.UpdateUsuario();
    } else {
      //Inserta
      this.AddUsuario();
    }
  }

  GetUsuarioById(id: number) {
    this.http.get<any>(`${this.API_URI}/Usuario/${id}`).subscribe(
      (response) => {
        console.log('Usuario cargado:', response);

        if (response.correct && response.object) {
          this.usuario = {
            idUsuario: response.object.idUsuario,
            Nombre: response.object.nombre,
            ApellidoPaterno: response.object.apellidoPaterno,
            ApellidoMaterno: response.object.apellidoMaterno,
            Imagen: response.object.imagen,
            FechaNacimiento: response.object.fechaNacimiento,
          };

          if (this.usuario.Imagen) {
            this.imagenPreview = 'data:image/jpeg;base64,' + this.usuario.Imagen;
          }
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Error al cargar datos',
            text: 'No se encontró información para este usuario.',
            confirmButtonText: 'Aceptar'
          });
        }
      },
      (error) => {
        console.error('Error al cargar usuario:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error al cargar datos',
          text: 'No se pudo cargar la información del usuario.',
          confirmButtonText: 'Aceptar'
        });
      }
    );
  }

  AddUsuario() {
    this.http.post(`${this.API_URI}/Usuario`, this.usuario).subscribe(
      (result) => {
        console.log('Usuario agregado con éxito:', result);
        Swal.fire({
          icon: 'success',
          title: 'Usuario agregado con éxito',
          text: 'El usuario ha sido registrado correctamente.',
          confirmButtonText: 'Aceptar'
        }).then(() => {
          this.router.navigate(['/getall']);
        });
        this.ClearForm();
      },
      (error) => {
        console.error('Error al guardar el usuario:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error al guardar el usuario',
          text: 'Ocurrió un error al intentar guardar los datos.',
          confirmButtonText: 'Aceptar'
        });
      }
    );
  }

  UpdateUsuario() {
    this.http.put(`${this.API_URI}/Usuario/${this.usuario.idUsuario}`, this.usuario).subscribe(
      (result) => {
        console.log('Usuario actualizado con éxito:', result);
        Swal.fire({
          icon: 'success',
          title: 'Usuario actualizado con éxito',
          text: 'Los cambios han sido guardados correctamente.',
          confirmButtonText: 'Aceptar'
        }).then(() => {
          this.router.navigate(['/getall']);
        });
      },
      (error) => {
        console.error('Error al actualizar el usuario:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error al actualizar el usuario',
          text: 'Ocurrió un error al intentar guardar los cambios.',
          confirmButtonText: 'Aceptar'
        });
      }
    );
  }

  ClearForm() {
    this.usuario = {
      Nombre: '',
      ApellidoPaterno: '',
      ApellidoMaterno: '',
      FechaNacimiento: ''
    };
    this.imagenPreview = null;
    this.archivoSeleccionado = null;
  }


}

