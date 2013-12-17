--防覇龍ヘリオスフィア
function c80200004.initial_effect(c)
	--cannot attack
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80200004,0))
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_CANNOT_ATTACK_ANNOUNCE)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
	e1:SetRange(LOCATION_MZONE)
	e1:SetCondition(c80200004.condition)
	e1:SetTargetRange(0,1)
	c:RegisterEffect(e1)
	--lv change
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80200004,1))
	e2:SetType(EFFECT_TYPE_IGNITION)
	e2:SetCountLimit(1)
	e2:SetRange(LOCATION_MZONE)
	e2:SetCondition(c80200004.con)
	e2:SetOperation(c80200004.op)
	c:RegisterEffect(e2)
end
function c80200004.condition(e,tp,eg,ep,ev,re,r,rp)
	return Duel.GetFieldGroupCount(tp,LOCATION_MZONE,0)==1 and Duel.GetFieldGroupCount(tp,0,LOCATION_HAND) < 5
end
function c80200004.filter(c)
	return c:IsRace(RACE_DRAGON) and c:GetLevel()==8
end
function c80200004.con(e,tp,eg,ep,ev,re,r,rp)
	return Duel.IsExistingMatchingCard(c80200004.filter,tp,LOCATION_MZONE,0,1,nil) and e:GetHandler():GetLevel()~=8
end
function c80200004.op(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	if c:IsFaceup() and c:IsRelateToEffect(e) then
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_CHANGE_LEVEL)
		e1:SetValue(8)
		e1:SetReset(RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END)
		c:RegisterEffect(e1)
	end
end