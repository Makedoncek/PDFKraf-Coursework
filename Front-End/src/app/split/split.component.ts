import {Component, ElementRef, ViewChild} from '@angular/core';
import { PDFDocument } from 'pdf-lib';
import {PdfSplitService} from "../services/pdf-split.service";

@Component({
  selector: 'app-split',
  templateUrl: './split.component.html',
  styleUrl: './split.component.css'
})
export class SplitComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  MaxPages: number | undefined;
  selectedFile: File | undefined;
  fileName: String | undefined;
  from = 1;
  to = 1;
  result: Blob | undefined

  constructor(private service: PdfSplitService) {
  }

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

  SendSplitRequest(){
    if (this.selectedFile)
       this.service.postSplitRequest(this.service.convertToRequest(this.selectedFile,this.from, this.to )).subscribe(blob  => {this.result = blob})
  }

  downloadFile() {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(this.result!);
    link.download = "splited.pdf";
    link.click();
  }

}
