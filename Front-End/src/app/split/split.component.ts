import {Component, ElementRef, ViewChild} from '@angular/core';
import { PDFDocument } from 'pdf-lib';

@Component({
  selector: 'app-split',
  templateUrl: './split.component.html',
  styleUrl: './split.component.css'
})
export class SplitComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  disabled = false;
  MaxPages: number | undefined;
  selectedFile: File | undefined;
  fileName: String | undefined;

  triggerInput() {
    this.fileInput.nativeElement.click();
  }

  onFileSelection(event: any) {
    this.selectedFile = event.target.files[0] as File;

      this.getNumPages()
      this.getPDFName()
  }

  private getNumPages() {
      if (this.selectedFile) {
        const reader = new FileReader();

        reader.onload = async (e: any) => {
          const pdfBytes = e.target.result;

          try {
            const pdfDoc = await PDFDocument.load(pdfBytes);
            this.MaxPages = pdfDoc.getPageCount();
          }
          catch (error) {
            this.fileName = "Wrong ABOBAS please help me, I am drowning under the wotar"
            this.MaxPages = undefined;
          }
        };

        reader.readAsArrayBuffer(this.selectedFile);
      }
  }

  getPDFName() {
    this.fileName = this.selectedFile?.name
  }
}
