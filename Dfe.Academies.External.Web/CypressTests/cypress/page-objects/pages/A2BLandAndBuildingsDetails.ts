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

    static selectCurrentPlannedBuildingWorksOptionNo()
    {   
        cy.get('#buildingWorksOptionNo').click()
        cy.get('#buildingWorksOptionNo').should('be.checked')
    }

    static selectSharedFacilitiesOptionNo()
    {
        cy.get('#sharedFacilitiesOptionNo').click()
        cy.get('#sharedFacilitiesOptionNo').should('be.checked')
    }

    static selectLandGrantsOptionNo()
    {
        cy.get('#landGrantsOptionNo').click()
        cy.get('#landGrantsOptionNo').should('be.checked')
    }

    static selectSchoolPFIOptionNo()
    {
        cy.get('#pfiSchemeOptionNo').click()
        cy.get('#pfiSchemeOptionNo').should('be.checked')
    }

    static selectPrioritySchoolBuildProgOptionNo()
    {
        cy.get('#SchoolBuildLandPriorityBuildingProgrammeNo').click()
        cy.get('#SchoolBuildLandPriorityBuildingProgrammeNo').should('be.checked')
    }

    static selectBuildSchoolsForFutureOptionNo()
    {
        cy.get('#SchoolBuildLandFutureProgrammeNo').click()
        cy.get('#SchoolBuildLandFutureProgrammeNo').should('be.checked')
    }


    static submitLandAndBuildingsDetails()
    {
    cy.submitLandAndBuildingsDetails()
    }
}