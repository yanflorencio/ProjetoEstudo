import { Component, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";

import { ClienteService } from "../cliente.service";

@Component({
    selector: 'app-cliente',
    templateUrl: 'cadastrar-cliente.component.html'
})
export class CadastrarClienteComponent implements OnInit{

    cliente : Object = new Object();

    formulario: FormGroup;

    constructor(private clienteService: ClienteService){ }

    ngOnInit(){

    }
}