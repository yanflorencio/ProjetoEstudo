import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AlterarClienteComponent } from './clientes/alterar-cliente/alterar-cliente.component';
import { CadastrarClienteComponent } from './clientes/cadastrar-cliente/cadastrar-cliente.component';

const routes: Routes = [
  { path: 'cadastro', component: CadastrarClienteComponent },
  { path: 'alterar', component: AlterarClienteComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
