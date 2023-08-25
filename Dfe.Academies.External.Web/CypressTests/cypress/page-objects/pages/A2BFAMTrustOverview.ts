import BasePage from "../BasePage"
export default class A2BFAMTrustOverview extends BasePage {
    static FAMTrustOverviewTrustNameCompleteElementsVisible()
    {
        cy.FAMTrustOverviewTrustNameCompleteElementsVisible()
    }

    static selectOpeningDate()
    {
        cy.contains('Opening date').click()
    }

    static FAMTrustOverviewOpeningDateCompleteElementsVisible()
    {
        cy.FAMTrustOverviewOpeningDateCompleteElementsVisible()
    }

    static selectReasonsForFormingTheTrust()
    {
        cy.contains('Reasons for forming the trust').click()
    }

    static FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible()
    {
        cy.FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible()
        
    }

    static selectPlansForGrowth()
    {
        cy.contains('Plans for growth').click()
    }

    static FAMTrustOverviewPlansForGrowthCompleteElementsVisible()
    {
        cy.FAMTrustOverviewPlansForGrowthCompleteElementsVisible()
    }

    static selectSchoolImprovementStrategy()
    {
        cy.contains('School improvement strategy').click()
    }

    static FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible()
    {
        cy.FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible()
    }

    static selectGovernanceStructure()
    {
        cy.contains('Governance structure').click()
    }

    static FAMTrustOverviewGovernanceStructureCompleteElementsVisible()
    {
        cy.FAMTrustOverviewGovernanceStructureCompleteElementsVisible()
    }

    static selectKeyPeople()
    {
        cy.contains('Key people').click()
    }

    static FAMTrustOverviewKeyPeopleCompleteElementsVisible()
    {
        cy.FAMTrustOverviewKeyPeopleCompleteElementsVisible()
    }

    static selectReturnToYourApplication()
    {
        cy.get('.govuk-button').click()
    }
}