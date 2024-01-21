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
  compressLevel: number = 1;
  selectedFile: File | undefined;
  fileName: String | undefined;
  result: Blob | undefined;

  triggerInput() {
    this.wipeData()
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
          this.fileName = "Invalid File Format: The uploaded file is not a PDF. Please ensure you are uploading a file with a '.pdf' extension and try again."
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
    console.log(this.compressLevel)
    if (this.selectedFile)
    this.service.postCompressRequest(this.selectedFile, this.compressLevel).subscribe(blob => {this.result = blob})
  }

  downloadFile() {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(this.result!);
    link.download =  "compressed_" + this.fileName;
    link.click();
    this.wipeData()
  }

  private wipeData() {
    this.result = undefined
    this.fileName = undefined
  }
}
