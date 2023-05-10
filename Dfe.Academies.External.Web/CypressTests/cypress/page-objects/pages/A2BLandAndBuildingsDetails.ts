import BasePage from "../BasePage"
export default class A2BLandAndBuildingsDetails extends BasePage {
    static landAndBuildingsDetailsElementsVisible()
    {
        cy.landAndBuildingsDetailsElementsVisible()
    }

    static fillLandAndBuildingsDetails()
    {
    cy.fillLandAndBuildingsDetails()
    }


    static submitLandAndBuildingsDetails()
    {
    cy.submitLandAndBuildingsDetails()
    }
}