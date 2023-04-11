import BasePage from "../BasePage"
export default class A2BLandAndBuildingsSummary extends BasePage {

    static landAndBuildingsSummaryElementsVisible()
    {
        cy.landAndBuildingsSummaryElementsVisible()
    }

    static selectLandAndBuildingsStartSection()
    {
        cy.selectLandAndBuildingsStartSection()
    }

    static landAndBuildingsSummaryCompleteElementsVisible()
    {
        cy.landAndBuildingsSummaryCompleteElementsVisible()
    }

    static submitLandAndBuildingsSummary()
    {
        cy.submitLandAndBuildingsSummary()
    }
}