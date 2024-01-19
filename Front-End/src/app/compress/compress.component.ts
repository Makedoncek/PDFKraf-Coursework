import {Component, ElementRef, ViewChild} from '@angular/core';
import {PDFDocument} from "pdf-lib";
import {PdfCompressService} from "../services/pdf-compress.service";

@Component({
  selector: 'app-compress',
  templateUrl: './compress.component.html',
  styleUrl: './compress.component.css'
})
export class CompressComponent {

  constructor(private service: PdfCompressService) {
  }

  @ViewChild('fileInput') fileInput!: ElementRef;
  compressLevel = 1;
  selectedFile: File | undefined;
  fileName: String | undefined;
  result: Blob | undefined;

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
          await PDFDocument.load(pdfBytes);
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


  formatLabel(value: number) {
    switch (value) {
      case 1:
        return 'Recommended';
      case 2:
        return 'Extreme';
      default:
        return 'Less';
    }
  }

  SendCompressRequest(){
    if (this.selectedFile)
    this.service.postCompressRequest(this.selectedFile, this.compressLevel!)
  }

  downloadFile() {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(this.result!);
    link.download = "compressed.pdf";
    link.click();
  }

}
