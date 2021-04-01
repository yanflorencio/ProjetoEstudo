import { Component, OnInit } from "@angular/core";
import { Cliente } from "src/app/models/cliente";

import { ClienteService } from "../cliente.service";

@Component({
    selector: 'app-cadastrar_cliente',
    templateUrl: 'cadastrar-cliente.component.html'
})
export class CadastrarClienteComponent implements OnInit{

    cliente : Cliente = new Cliente();

    constructor(private clienteService: ClienteService){ }

    ngOnInit(){

    }

    cadastrarCliente(){
        this.clienteService.postCadastrarCliente(this.cliente)
            .subscribe(retorno => console.log(retorno));
    }
}