import { Component } from '@angular/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent {
  constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      'compression',
      this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/compression.svg')
    )
    this.matIconRegistry.addSvgIcon(
      'merge',
      this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/merge.svg')
    )
    this.matIconRegistry.addSvgIcon(
      'split-files',
      this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/split-files.svg')
    )
    this.matIconRegistry.addSvgIcon(
      'watermark',
      this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/watermark.svg')
    )
  }
}
