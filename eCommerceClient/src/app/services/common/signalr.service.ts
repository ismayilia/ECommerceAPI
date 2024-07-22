import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  constructor(@Inject("baseSignalRUrl") private baseSignalRUrl: string) { }

  private _connection: HubConnection;

  get connection(): HubConnection {
    return this._connection;
  }


  //bashladilmish bir hub verir
  start(hubUrl: string) {

    hubUrl = this.baseSignalRUrl + hubUrl

    if (!this.connection || this._connection?.state == HubConnectionState.Disconnected) {
      const builder: HubConnectionBuilder = new HubConnectionBuilder();

      const hubConnection: HubConnection = builder.withUrl(hubUrl)
        .withAutomaticReconnect()
        .build();

      hubConnection.start()
        .then(() => console.log("Connected"))
        .catch(error => setTimeout(() => this.start(hubUrl), 2000));

      this._connection = hubConnection;

    }

    this._connection.onreconnected(connectionId => console.log("Reconnected"));
    this._connection.onreconnecting(error => console.log("Reconnecting")); //qopan baglantinin tekrardan berpa olunma muddetinde oldugun bildiri
    this._connection.onclose(error => console.log("Close reconnection"));// tekrar baglantida qurulmadisa
  }

  //Signalr uzerinden bir clinetden diger client-e mesaj gonderme
  invoke(procedureName: string, message: any, successCallback?: (value) => void, errorCallback?: (error) => void) {

    this.connection.invoke(procedureName, message)
      .then(successCallback)
      .catch(errorCallback);

  }

  //serverden gelen anliq mesajlari runtime olaraq tutmasina sebeb olur
  // ...mesage-> c#daki params
  on(procedureName: string, callBack: (...message: any) => void) {
    this.connection.on(procedureName, callBack);
  }

}
