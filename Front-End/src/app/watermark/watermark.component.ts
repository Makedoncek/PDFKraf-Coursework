import {Component, ElementRef, ViewChild} from '@angular/core';
import {PDFDocument} from "pdf-lib";

@Component({
  selector: 'app-watermark',
  templateUrl: './watermark.component.html',
  styleUrl: './watermark.component.css'
})
export class WatermarkComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  value = '';
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
        }
        catch (error) {
          this.fileName = "Wrong ABOBAS please help me, I am drowning under the wotar"
        }
      };

      reader.readAsArrayBuffer(this.selectedFile);
    }
  }

  getPDFName() {
    this.fileName = this.selectedFile?.name
  }
}
