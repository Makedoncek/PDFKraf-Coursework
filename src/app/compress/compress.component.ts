import {Component, ElementRef, ViewChild} from '@angular/core';
import {PDFDocument} from "pdf-lib";

@Component({
  selector: 'app-compress',
  templateUrl: './compress.component.html',
  styleUrl: './compress.component.css'
})
export class CompressComponent {
  disabled = true;
  @ViewChild('fileInput') fileInput!: ElementRef;
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
    this.disabled = false
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

  redirectToRickRoll() {
    window.location.href = 'https://www.youtube.com/watch?v=HEyqytq0-is';

  }

  formatLabel(value: number) {
    switch (value) {
      case 1:
        return 'Loh';
      case 2:
        return 'Popusk';
      default:
        return 'Debich';
    }
  }


}
