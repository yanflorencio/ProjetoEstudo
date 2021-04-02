import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CadastrarClienteComponent } from "./cadastrar-cliente/cadastrar-cliente.component";
import { AlterarClienteComponent } from './alterar-cliente/alterar-cliente.component';
import { CommonModule } from "@angular/common";

@NgModule({
    declarations: [ 
                    CadastrarClienteComponent, 
                    AlterarClienteComponent
                ],
    exports: [
        CadastrarClienteComponent,
        AlterarClienteComponent
    ],
    imports: [
        FormsModule,
        CommonModule
    ]
})
export class ClientesModule {}