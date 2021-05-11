import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Cliente } from "../models/cliente";

const PATH_CONTROLLER = '/api/Cliente';

@Injectable({providedIn: 'root'}/* Especifica que existirá apenas uma instância deste compoente, como um singleton*/)
export class ClienteService{

    constructor (private http: HttpClient){} 

    postCadastrarCliente(cliente: Cliente){
        return this.http.post(PATH_CONTROLLER, cliente);
    }

    getBuscarClienteByCpf(cpf: string){
        return this.http.get<Cliente>(PATH_CONTROLLER + '/ByCpf/' + cpf);
    }

    putAlterarCliente(cliente: Cliente){
        return this.http.put(PATH_CONTROLLER, cliente);
    }

    deleteDeletarCliente(id: number){
        return this.http.delete(PATH_CONTROLLER + `/${id}`);
    }

}