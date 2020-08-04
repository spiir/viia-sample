import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, combineLatest } from 'rxjs';
import { finalize, map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'platform',
})
export class GlobalSpinnerService {
  private readonly active = new BehaviorSubject(true);
  private readonly tracker = new BehaviorSubject([]);

  public readonly active$ = combineLatest([
    this.active,
    this.tracker.pipe((map((arr: any[]) => !!arr.length)) as any),
  ]).pipe((map((arr: any[]) => !arr.filter((v) => !v).length)) as any);

  public disableUntil(input: Observable<any>): void {
    this.active.next(false);
    input.pipe(take(1)).subscribe(() => this.active.next(true));
  }

  public track<T>(input: Observable<T>): Observable<T> {
    this.tracker.next([...this.tracker.value, input]);
    return input.pipe(
      finalize(() => {
        const index = this.tracker.value.indexOf(input);
        delete this.tracker.value[index];
        this.tracker.next(this.tracker.value.filter((x) => x !== undefined));
      })
    );
  }
}
