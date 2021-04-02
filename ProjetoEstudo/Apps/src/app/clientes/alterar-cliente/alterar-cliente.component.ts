import { Component, OnInit } from '@angular/core';
import { Cliente } from 'src/app/models/cliente';
import { ClienteService } from '../cliente.service';

@Component({
  selector: 'app-alterar-cliente',
  templateUrl: './alterar-cliente.component.html',
  styleUrls: ['./alterar-cliente.component.css']
})
export class AlterarClienteComponent implements OnInit {

  cpf: string;
  cliente: Cliente;

  constructor(private clienteService: ClienteService) { }

  ngOnInit(): void {
  }

  buscarClientePorCpf(){
    this.clienteService.getBuscarClienteByCpf(this.cpf)
                        .subscribe(clienteRetorno => {
                                    if(clienteRetorno != null){
                                        this.cliente = clienteRetorno
                                    }
                        });
  }

}
