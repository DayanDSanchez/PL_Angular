import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormComponent } from '../components/form/form.component';
import { TablaComponent } from '../components/tabla/tabla.component';

const routes: Routes = [
  { path: '', component: TablaComponent },
  { path: 'getall', component: TablaComponent },
  { path: 'form',component: FormComponent},
  { path: 'form/:idUsuario',component: FormComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
