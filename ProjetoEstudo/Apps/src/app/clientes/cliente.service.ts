import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Cliente } from "../models/cliente";

const PATH_CONTROLLER = '/api/Cliente';

@Injectable({providedIn: 'root'}/* Especifica que existirá apenas uma instância deste compoente, como um singleton*/)
export class ClienteService{

    constructor (private http: HttpClient){
        this.getClienteAndJogosAlugadosByCpf('83401887041');
    }

    getClienteAndJogosAlugadosByCpf(cpf: string){
        return this.http.get<Object>(PATH_CONTROLLER + '/JogosAlugadosByCpf/' + cpf);
    }

    postCadastrarCliente(cliente: Cliente){
        return this.http.post(PATH_CONTROLLER, cliente);
    }
}