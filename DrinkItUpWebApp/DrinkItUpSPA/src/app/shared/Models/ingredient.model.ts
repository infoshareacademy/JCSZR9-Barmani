import { UnitModel } from "./unit.model";

export class IngredientModel{
    constructor(
        public unit: UnitModel,
        public unitId?: number,
        public ingredientId?: number,
        public name?: string,
        public quantity?: number,
        public isUsed?: boolean
    ){}
}