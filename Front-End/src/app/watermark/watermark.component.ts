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
  watermarkText: string | undefined;
  selectedFile: File | undefined;
  fileName: String | undefined;
  result: Blob | undefined;

  triggerInput() {
    this.wipeData();
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
          this.fileName = "Invalid File Format: The uploaded file is not a PDF. Please ensure you are uploading a file with a '.pdf' extension and try again."
          this.selectedFile = undefined
        }
      };

      reader.readAsArrayBuffer(this.selectedFile);
    }
  }

  private getPDFName() {
    this.fileName = this.selectedFile?.name
  }

  SendWatermarkRequest(){
    if (this.selectedFile)
      this.service.postWatermarkRequest(this.selectedFile, this.watermarkText!).subscribe( blob => {this.result = blob})
  }


  downloadFile() {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(this.result!);
    link.download = "watermarked_" + this.fileName;
    link.click();
    this.wipeData()
  }

  private wipeData() {
    this.result = undefined
    this.fileName = undefined
    this.watermarkText = undefined
  }
}
