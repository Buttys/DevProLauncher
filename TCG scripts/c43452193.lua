--異次元トンネル－ミラーゲート－
--TCG Version
function c43452193.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_CONTROL)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_ATTACK_ANNOUNCE)
	e1:SetCondition(c43452193.condition)
	e1:SetTarget(c43452193.target)
	e1:SetOperation(c43452193.activate)
	c:RegisterEffect(e1)
end
function c43452193.condition(e,tp,eg,ep,ev,re,r,rp)
	local at=Duel.GetAttackTarget()
	return Duel.GetTurnPlayer()~=tp and at and at:IsFaceup() and at:IsSetCard(0x3008)
end
function c43452193.target(e,tp,eg,ep,ev,re,r,rp,chk)
	local a=Duel.GetAttacker()
	local at=Duel.GetAttackTarget()
	if chk==0 then return a:IsOnField() and a:IsAbleToChangeControler()
		and at:IsOnField() and at:IsAbleToChangeControler() end
	local g=Group.FromCards(a,at)
	Duel.SetTargetCard(g)
	Duel.SetOperationInfo(0,CATEGORY_CONTROL,g,2,0,0)
end
function c43452193.activate(e,tp,eg,ep,ev,re,r,rp)
	local a=Duel.GetAttacker()
	local at=Duel.GetAttackTarget()
	if a:IsRelateToEffect(e) and at:IsRelateToEffect(e) and a:IsFaceup() and at:IsFaceup() then
		if Duel.SwapControl(a,at) then
		
			Duel.CalculateDamage(a,at)
			
			local g=Group.FromCards(a,at)
			g:KeepAlive()
			a:RegisterFlagEffect(43452193,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END,0,1)
			at:RegisterFlagEffect(43452193,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END,0,1)

			local e1=Effect.CreateEffect(e:GetHandler())
			e1:SetCategory(CATEGORY_CONTROL)
			e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
			e1:SetCode(EVENT_PHASE+PHASE_END)
			e1:SetCountLimit(1)
			e1:SetLabelObject(g)
			e1:SetReset(RESET_PHASE+PHASE_END)
			e1:SetOperation(c43452193.swapop)
			Duel.RegisterEffect(e1,0)
		end
	end
end

function c43452193.filter(c)
	return c:GetFlagEffect(43452193)>0
end
function c43452193.swapop(e,tp,eg,ep,ev,re,r,rp)
	local g=e:GetLabelObject():Filter(c43452193.filter,nil)
	local a=g:GetFirst()
	local at=g:GetNext()
	g:DeleteGroup()
	if a and at then
		Duel.SwapControl(a,at)
	end
end