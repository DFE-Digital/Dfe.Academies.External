class TrustOverview {
  public checkSectionCompleted(section: string): this {
    switch (section) {
      case 'Trust name':
        cy.get('[data-cy="sectionStatus"]').eq(0).contains('Completed')
        break
      case 'Opening date':
        cy.get('[data-cy="sectionStatus"]').eq(1).contains('Completed')
        break
      case 'Reasons for forming the trust':
        cy.get('[data-cy="sectionStatus"]').eq(2).contains('Completed')
        break
      case 'Plans for growth':
        cy.get('[data-cy="sectionStatus"]').eq(3).contains('Completed')
        break
      case 'School improvement strategy':
        cy.get('[data-cy="sectionStatus"]').eq(4).contains('Completed')
        break
      case 'Governance structure':
        cy.get('[data-cy="sectionStatus"]').eq(5).contains('Completed')
        break
      case 'Key people':
        cy.get('[data-cy="sectionStatus"]').eq(6).contains('Completed')
        break
      default:
        break
    }
    return this
  }

  public selectOpeningDate(): this {
    cy.contains('Opening date').click()

    return this
  }

  public selectReasonsForFormingTheTrust(): this {
    cy.contains('Reasons for forming the trust').click()

    return this
  }

  public selectPlansForGrowth(): this {
    cy.contains('Plans for growth').click()

    return this
  }

  public selectSchoolImprovementStrategy(): this {
    cy.contains('School improvement strategy').click()

    return this
  }

  public selectGovernanceStructure(): this {
    cy.contains('Governance structure').click()

    return this
  }

  public selectKeyPeople(): this {
    cy.contains('Key people').click()

    return this
  }

  public selectReturnToYourApplication(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const trustOverview = new TrustOverview()

export default trustOverview
