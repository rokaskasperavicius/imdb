import type { paths } from "../../schema";

export type AllPeopleType =
  paths["/api/People"]["get"]["responses"]["200"]["content"]["application/json"];

export type PersonType = NonNullable<AllPeopleType["data"]>[number];
