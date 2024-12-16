class PreOpeningSupportGrantSummary {
  public startPreopeningSupportGrant(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public checkPreopeningSupportGrantSummaryCompleted(): this {
    cy.get('[data-cy="response"]').contains('To the school')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }

  public warningIcon(): this {
    cy.get('span[class="govuk-warning-text__icon"]')

    return this
  }

  public warningText(): this {
    cy.get('strong[class="govuk-warning-text__text"]')

    return this
  }
}

const preOpeningSupportGrantSummary = new PreOpeningSupportGrantSummary()

export default preOpeningSupportGrantSummary
