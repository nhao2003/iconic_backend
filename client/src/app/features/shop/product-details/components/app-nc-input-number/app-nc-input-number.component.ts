import { CommonModule } from '@angular/common';
import {
  Component,
  Input,
  Output,
  EventEmitter,
  forwardRef,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-nc-input-number',
  template: `
    <div
      class="nc-NcInputNumber flex items-center justify-between space-x-5"
      [ngClass]="className"
    >
      <div *ngIf="label" class="flex flex-col">
        <span class="font-medium text-neutral-800 dark:text-neutral-200">{{
          label
        }}</span>
        <span
          *ngIf="desc"
          class="text-xs text-neutral-500 dark:text-neutral-400 font-normal"
          >{{ desc }}</span
        >
      </div>

      <div
        class="nc-NcInputNumber__content flex items-center justify-between w-[104px] sm:w-28"
      >
        <button
          class="w-8 h-8 rounded-full flex items-center justify-center border border-neutral-400 dark:border-neutral-500 bg-white dark:bg-neutral-900"
          (click)="decrement()"
          [disabled]="value <= min"
        >
          <mat-icon>remove</mat-icon>
        </button>
        <span class="select-none block flex-1 text-center leading-none">{{
          value
        }}</span>
        <button
          class="w-8 h-8 rounded-full flex items-center justify-center border border-neutral-400 dark:border-neutral-500 bg-white dark:bg-neutral-900"
          (click)="increment()"
          [disabled]="value >= max"
        >
          <mat-icon>add</mat-icon>
        </button>
      </div>
    </div>
  `,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AppNcInputNumberComponent),
      multi: true,
    },
  ],
  standalone: true,
  imports: [CommonModule, MatIcon],
})
export class AppNcInputNumberComponent implements ControlValueAccessor {
  @Input() className = 'w-full';
  @Input() defaultValue = 1;
  @Input() min = 1;
  @Input() max = 99;
  @Input() label?: string;
  @Input() desc?: string;
  @Output() onChange = new EventEmitter<number>();

  value = this.defaultValue;

  private onTouched = () => {};
  private onChangeFn = (value: number) => {};

  writeValue(value: number): void {
    this.value = value || this.defaultValue;
  }

  registerOnChange(fn: (value: number) => void): void {
    this.onChangeFn = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // Handle the disabled state here
  }

  increment(): void {
    if (this.max && this.value < this.max) {
      this.value++;
      this.onChange.emit(this.value);
      this.onChangeFn(this.value);
    }
  }

  decrement(): void {
    if (this.value > this.min) {
      this.value--;
      this.onChange.emit(this.value);
      this.onChangeFn(this.value);
    }
  }
}
