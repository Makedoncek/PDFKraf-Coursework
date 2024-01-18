import {Component, ElementRef, ViewChild} from '@angular/core';
import {PDFDocument} from "pdf-lib";
import {PdfWatermarkService} from "../services/pdf-watermark.service";

@Component({
  selector: 'app-watermark',
  templateUrl: './watermark.component.html',
  styleUrl: './watermark.component.css'
})
export class WatermarkComponent {

  constructor(private service: PdfWatermarkService) {
  }

  @ViewChild('fileInput') fileInput!: ElementRef;
  watermarkText = '';
  selectedFile: File | undefined;
  fileName: String | undefined;
  result: File | undefined;

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
        } catch (error) {
          this.fileName = "Wrong ABOBAS please help me, I am drowning under the wotar"
        }
      };

      reader.readAsArrayBuffer(this.selectedFile);
    }
  }

  getPDFName() {
    this.fileName = this.selectedFile?.name
  }

  SendWatermarkRequest(){
    if (this.selectedFile)
      this.service.postWatermarkRequest(this.selectedFile, this.watermarkText).subscribe( it => {this.result = it})
  }


  downloadFile() {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(this.result!);
    link.download = "watermarked.pdf";
    link.click();
  }
}
