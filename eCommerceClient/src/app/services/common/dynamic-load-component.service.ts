import { ComponentFactoryResolver, Injectable, ViewContainerRef } from '@angular/core';
import { BaseComponent } from '../../base/base.component';

@Injectable({
  providedIn: 'root'
})
export class DynamicLoadComponentService {

  // ViewContainerRef - dinamik olaraq yuklenecek componenti ozunde saxlayan container-dir (Her dinamik yuklemede evvelki view-lari
  // clear etmemeiz lazimdir)
  // ComponentFactory - componentlerin instance-lari yaratmaq ucun istifade ediler bir ojectdir
  // ComponentFactoryResolver - belirli bir component ucun ComponentFactory`i resolve eden bir classdir(sinifdir)-
  // icerisindeki resolveComponentFactory funksiyasi vasitesile elaqeli componente aid bir ComponentFactory objecti yaradib, qaytarir 

  constructor() { }

  async loadComponent(component: ComponentType, viewContainerRef: ViewContainerRef) {
    let _component: any = null;
    switch (component) {
      case ComponentType.BasketsComponent:
        _component = (await import("../../ui/components/baskets/baskets.component")).BasketsComponent
        break;
    }
    viewContainerRef.clear();
    return viewContainerRef.createComponent(_component);

  }
}

export enum ComponentType {
  BasketsComponent
}
