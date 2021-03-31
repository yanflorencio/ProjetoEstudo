import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CadastrarClienteComponent } from './clientes/cadastrar-cliente/cadastrar-cliente.component';

const routes: Routes = [
  { path: 'cadastro', component: CadastrarClienteComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
