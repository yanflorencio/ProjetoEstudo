import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CadastrarClienteComponent } from "./cadastrar-cliente/cadastrar-cliente.component";

@NgModule({
    declarations: [CadastrarClienteComponent],
    exports: [CadastrarClienteComponent],
    imports: [FormsModule]
})
export class ClientesModule{

}