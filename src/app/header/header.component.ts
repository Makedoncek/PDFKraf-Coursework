import { Component } from '@angular/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
  this.matIconRegistry.addSvgIcon(
    'logo',
    this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/merge.svg')
  )
}

}
