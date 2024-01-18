import {Component, ElementRef, ViewChild} from '@angular/core';
import { PDFDocument } from 'pdf-lib';
import {PdfConvertorService} from "../pdf-convertor.service";

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
  result: File | undefined

  constructor(private service: PdfConvertorService) {
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
       this.service.postSplitRequest(this.service.convertToRequest(this.selectedFile,this.from, this.to )).subscribe(file  => {this.result = file})
  }

  downloadFile() {
    const blob = new Blob([this.result!], { type: this.result!.type });

    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = this.result!.name;
    link.click();
  }

}
