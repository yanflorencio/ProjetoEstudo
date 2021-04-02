import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CadastrarClienteComponent } from "./cadastrar-cliente/cadastrar-cliente.component";
import { AlterarClienteComponent } from './alterar-cliente/alterar-cliente.component';

@NgModule({
    declarations: [ 
                    CadastrarClienteComponent, 
                    AlterarClienteComponent
                ],
    exports: [CadastrarClienteComponent],
    imports: [FormsModule]
})
export class ClientesModule {}