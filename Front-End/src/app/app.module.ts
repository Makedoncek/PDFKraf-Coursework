import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { NavigationComponent } from './navigation/navigation.component';
import { FooterComponent } from './footer/footer.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {HttpClientModule} from "@angular/common/http";
import { WatermarkComponent } from './watermark/watermark.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule, NgControl, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { CompressComponent } from './compress/compress.component';
import {MatSliderModule} from "@angular/material/slider";
import { SplitComponent } from './split/split.component';
import { MergeComponent } from './merge/merge.component';
import {FileSizePipe} from "./pipes/file-size.pipe";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NavigationComponent,
    FooterComponent,
    WatermarkComponent,
    CompressComponent,
    SplitComponent,
    MergeComponent,
    FileSizePipe,
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        MatIconModule,
        MatButtonModule,
        MatToolbarModule,
        MatSidenavModule,
        HttpClientModule,
        MatInputModule,
        FormsModule,
        BrowserAnimationsModule,
        MatSliderModule,
        ReactiveFormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
