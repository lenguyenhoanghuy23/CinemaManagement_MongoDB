import { mapEnumToOptions } from '@abp/ng.core';

export enum StatusEmu {
  active = 0,
  inactive = 1,
}

export const statusEmuOptions = mapEnumToOptions(StatusEmu);
