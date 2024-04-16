import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { HttpClientService } from '../../services/common/http-client.service';
import { ProductService } from '../../services/common/modedls/product.service';
import { BaseComponent, SpinnerType } from '../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

declare var $: any;

@Directive({
  selector: '[appDelete]',
  standalone: true
})
export class DeleteDirective {

  constructor(private element: ElementRef,
    private _renderer: Renderer2,
    private productService: ProductService,
    private spinner: NgxSpinnerService
  ) {
    const img = _renderer.createElement("img");
    img.setAttribute("src", "../../../../../assets/delete.png");
    img.setAttribute("style", "cursor: pointer;");
    img.with = 25;
    img.height = 25;
    _renderer.appendChild(element.nativeElement, img);
  }

  @Input() id: string
  @Output() callback: EventEmitter<any> = new EventEmitter<any>;

  @HostListener("click")

  async onclick() {
    this.spinner.show(SpinnerType.BallAtom);
    const td: HTMLTableElement = this.element.nativeElement;
    await this.productService.delete(this.id);

    // $(td.parentElement).fadeOut( () => {
    //   this.callback.emit();
    // })
    td.parentElement.style.display = 'none';
    this.callback.emit();

  }

}
