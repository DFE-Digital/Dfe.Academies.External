class SchoolOverview {
  public checkSectionCompleted(section: string): this {
    switch (section) {
      case 'About the conversion':
        cy.get('[data-cy="sectionStatus"]').eq(0).contains('Completed')
        break
      case 'Further information':
        cy.get('[data-cy="sectionStatus"]').eq(1).contains('Completed')
        break
      case 'Finances':
        cy.get('[data-cy="sectionStatus"]').eq(2).contains('Completed')
        break
      case 'Future pupil numbers':
        cy.get('[data-cy="sectionStatus"]').eq(3).contains('Completed')
        break
      case 'Land and buildings':
        cy.get('[data-cy="sectionStatus"]').eq(4).contains('Completed')
        break
      case 'Consultation':
        cy.get('[data-cy="sectionStatus"]').eq(5).contains('Completed')
        break
      case 'Declaration':
        cy.get('[data-cy="sectionStatus"]').eq(6).contains('Completed')
        break
      default:
        cy.log('Invalid option given')
        break
    }
    return this
  }

  public saveAndReturn(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const schoolOverview = new SchoolOverview()

export default schoolOverview
